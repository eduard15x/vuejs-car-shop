<template>
    <div class="login-alert-message" v-if="loginShowAlert" :class="loginAlertVariant">
        {{ loginAlertMessage }}
    </div>
    <vee-form class="login-form" @submit="login" :validation-schema="loginSchema">
        <div class="login-form__field">
            <label>Email</label>
            <vee-field
                type="email"
                class="login-form__field--input"
                placeholder="Enter Email"
                name="email"
            />
            <ErrorMessage class="error" name="email" />
        </div>
        <div class="login-form__field">
            <label>Password</label>
            <vee-field
                type="password"
                class="login-form__field--input"
                placeholder="Password"
                name="password"
            />
            <ErrorMessage class="error" name="password" />
        </div>

        <button
            :disabled="loginInSubmission"
            type="submit"
            class="login-form__submit-btn"
        >
            Submit
        </button>
    </vee-form>
</template>

<script>
import { mapStores } from 'pinia';
import useUserStore from '@/stores/user';
import cookies from 'js-cookie'

export default {
    name: 'LoginForm',
    data() {
        return {
            loginSchema: {
                email: "required|email",
                password: "required|min:9|max:100",
            },
            loginInSubmission: false,
            loginShowAlert: false,
            loginAlertVariant: 'loading-variant',
            loginAlertMessage: 'Please wait. Processing...',
        }
    },
    computed: {
        ...mapStores(useUserStore),
    },
    methods: {
        async login(values) {
            this.loginInSubmission = true;
            this.loginShowAlert = true;
            this.loginAlertVariant = 'loading-variant';
            this.loginAlertMessage = 'Please wait. Processing...';

            try {
                const response = await fetch('https://localhost:7090/api/user/login', {
                    method: "POST",
                    headers: {"Content-Type": "application/json"},
                    credentials: 'include',
                    body: JSON.stringify(values)
                });

                const json = await response.json();
                console.log(json);

                if (json.statusCode === 200) {
                    const { email, userId, isLoggedIn } = json.value;

                    this.userStore.email = email;
                    this.userStore.userId = userId;
                    this.userStore.isLoggedIn = isLoggedIn;

                    const expireTime30Minutes = new Date(new Date().getTime() + 30 * 60 * 1000);
                    const cookieString = userId + "-" + email;

                    cookies.set("user", cookieString, {expires: expireTime30Minutes, path: "/", secure: true});

                } else {
                    this.loginInSubmission = false;
                    this.loginAlertVariant = 'error-variant';
                    this.loginAlertMessage = json.value;
                    return;
                }
            } catch(err) {
                this.loginInSubmission = false;
                this.loginAlertVariant = 'error-variant';
                this.loginAlertMessage = err;
                return;
            }

            this.loginAlertVariant = 'success-variant';
            this.loginAlertMessage = 'Success. You are logged in';

            setTimeout(() => {
                window.location.reload();
            }, 1000);
        }
    }
}
</script>