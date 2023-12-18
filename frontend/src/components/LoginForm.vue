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
            loginAlertVariant: 'bg-blue-500',
            loginAlertMessage: 'Please wait. Processing...',
        }
    },
    methods: {
        login(values) {
            console.log(values);
        }
    }
}
</script>