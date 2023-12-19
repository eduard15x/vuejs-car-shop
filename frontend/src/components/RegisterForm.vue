<template>
<!-- Registration Form -->
    <div
        class="register-alert-message"
        v-if="registrationShowAlert"
        :class="registrationAlertVariant"
    >
        {{ registrationAlertMessage }}
    </div>

    <vee-form class="register-form" :validation-schema="registerSchema" @submit="register" :initial-values="defaultUserValues">
        <!-- Name -->
        <div class="register-form__field">
            <label>Name</label>
            <vee-field
                type="text"
                class="register-form__field--input"
                placeholder="Enter Name"
                name="name"
            />
            <ErrorMessage class="error" name="name" />
        </div>
        <!-- Email -->
        <div class="register-form__field">
            <label class="inline-block mb-2">Email</label>
            <vee-field
                type="email"
                class="register-form__field--input"
                placeholder="Enter Email"
                name="email"
            />
            <ErrorMessage class="error" name="email" />
        </div>
        <!-- Age -->
        <div class="register-form__field">
            <label class="inline-block mb-2">Age</label>
            <vee-field
                type="number"
                class="register-form__field--input"
                placeholder="Enter You Age (+18)"
                name="age"
            />
            <ErrorMessage class="error" name="age" />
        </div>
        <!-- Phone number -->
        <div class="register-form__field">
            <label class="inline-block mb-2">Phone number</label>
            <vee-field
                type="string"
                class="register-form__field--input"
                placeholder="Enter Your Number"
                name="phoneNumber"
            />
            <ErrorMessage class="error" name="phoneNumber" />
        </div>
        <!-- Password -->
        <div class="register-form__field">
            <label class="inline-block mb-2">Password</label>
            <vee-field
                name="password"
                :bails="false"
                v-slot="{ field, errors }"
            >
                <input
                    type="password"
                    class="register-form__field--input"
                    placeholder="Password"
                    v-bind="field"
                />
                <div class="error"  v-for="error in errors" :key="error">
                    {{ error }}
                </div>
            </vee-field>
        </div>
        <!-- Confirm Password -->
        <div class="register-form__field">
            <label class="inline-block mb-2">Confirm Password</label>
            <vee-field
                type="password"
                class="register-form__field--input"
                placeholder="Confirm Password"
                name="confirmPassword"
            />
            <ErrorMessage class="error" name="confirmPassword" />
        </div>
        <!-- TOS -->
        <div class="register-form__tos">
            <vee-field
                type="checkbox"
                name="tos"
                id="tos"
                value="1"
            />
            <label for="tos">Accept terms of service</label>
        </div>
        <ErrorMessage class="error-tos" name="tos" />
        <button
            :disabled="registrationInSubmission"
            type="submit"
            class="register-form__submit-btn"
        >
            Submit
        </button>
    </vee-form>
</template>

<script>
export default {
    name: 'RegisterForm',
    data() {
        return {
            registerSchema: {
                name: "required|min:3|max:100|alpha_spaces",
                email: "required|min:3|max:100|email",
                age: "required|min_value:18|max_value:100",
                password: "required|min:9|max:100|excluded:password",
                confirmPassword: "confirmed:@password",
                phoneNumber: "required|min:10|max:10",
                tos: "required",
            },
            registrationInSubmission: false,
            registrationShowAlert: false,
            registrationAlertVariant: 'loading-variant',
            registrationAlertMessage: 'Please wait. Processing...',
        }
    },
    methods: {
        async register(values) {
            this.registrationInSubmission = true;
            this.registrationShowAlert = true;
            this.registrationAlertVariant = 'loading-variant';
            this.registrationAlertMessage = 'Please wait. Processing...';


            console.log(values);
            let data;

            try {
                const response = await fetch('https://localhost:7090/api/user/register', {
                    method: "POST",
                    headers: {"Content-Type": "application/json"},
                    body: JSON.stringify(values)
                });

                const json = await response.json();
                console.log(json);

                if (json.statusCode === 201 && json.value.isLoggedIn) {
                    data = json.value;
                } else {
                    this.registrationInSubmission = false;
                    this.registrationAlertVariant = 'error-variant';
                    this.registrationAlertMessage = json.value;
                    return;
                }
            } catch(err) {
                this.registrationInSubmission = false;
                this.registrationAlertVariant = 'error-variant';
                this.registrationAlertMessage = err;
                return;
            }

            this.registrationAlertVariant = 'success-variant';
            this.registrationAlertMessage = 'Account created. You will be logged in.';
        }
    }
}
</script>