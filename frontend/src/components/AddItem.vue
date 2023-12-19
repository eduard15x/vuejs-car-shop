<template>
    <div class="manage-item-alert-message" v-if="addShowAlert" :class="addAlertVariant">
        {{ addAlertMessage }}
    </div>
    <vee-form class="add-form" @submit="addItem" :validation-schema="carItemSchema">
        <div class="add-form__field">
            <label>Car Name</label>
            <vee-field
                type="text"
                class="add-form__field--input"
                placeholder="Name"
                name="carName"
            />
            <ErrorMessage class="error" name="carName" />
        </div>
        <div class="add-form__field">
            <label>Car Model</label>
            <vee-field
                type="text"
                class="add-form__field--input"
                placeholder="Model"
                name="carModel"
            />
            <ErrorMessage class="error" name="carModel" />
        </div>
        <div class="add-form__field">
            <label>Car Year</label>
            <vee-field
                type="number"
                class="add-form__field--input"
                placeholder="Year"
                name="carYear"
            />
            <ErrorMessage class="error" name="carYear" />
        </div>
        <div class="add-form__field">
            <label>Car Price</label>
            <vee-field
                type="number"
                class="add-form__field--input"
                placeholder="Price"
                name="carPrice"
            />
            <ErrorMessage class="error" name="carPrice" />
        </div>
        <div class="update-form__field">
            <label>Car Engine</label>
            <vee-field
                type="text"
                class="update-form__field--input"
                placeholder="Engine"
                name="engine"
            />
            <ErrorMessage class="error" name="engine" />
        </div>

        <button
            :disabled="addInSubmission"
            type="submit"
            class="add-form__submit-btn"
        >
            Add item
        </button>
    </vee-form>
</template>

<script>
import { mapStores } from 'pinia';
import useUserStore from '@/stores/user';
import useManageModalStore from '@/stores/manageModal';

export default {
    name: 'AddItem',
    props: {
        fetchItems: {
            type: Function,
            required: true
        },
        action: {
            type: Function,
            required: true
        }
    },
    data() {
        return {
            carItemSchema: {
                carName: "required|min:3|max:100",
                carModel: "required|min:3|max:100",
                carYear: "required|min_value:2015|max_value:2024",
                carPrice: "required|min_value:5000|max_value:5000000",
                engine: "required|min:3|max:100"
            },
            addInSubmission: false,
            addShowAlert: false,
            addAlertVariant: 'loading-variant',
            addAlertMessage: 'Please wait. Processing...',
        }
    },
    computed: {
        ...mapStores(useUserStore, useManageModalStore),
    },
    methods: {
        async addItem(values) {
            this.addInSubmission = true;
            this.addShowAlert = true;
            this.addAlertVariant = 'loading-variant';
            this.addAlertMessage = 'Please wait. Processing...';

            values.userId = this.userStore.userId ? this.userStore.userId : 0;

            try {
                const response = await fetch(`https://localhost:7090/api/cars/add`, {
                    method: 'POST',
                    headers: {"Content-Type": "application/json"},
                    credentials: 'include',
                    body: JSON.stringify(values)
                })
                const json = await response.json();

                if (json.statusCode !== 200) {
                    this.addInSubmission = false;
                    this.addAlertVariant = 'error-variant';
                    this.addAlertMessage = json.value;
                    return;
                }
            } catch (error) {
                this.addInSubmission = false;
                this.addAlertVariant = 'error-variant';
                this.addAlertMessage = error;
                return;
            }

            this.addAlertVariant = 'success-variant';
            this.addAlertMessage = 'Product added successfully.';
            await this.action();

            setTimeout(() => {
                this.manageModalStore.isOpen = !this.manageModalStore.isOpen;
            }, 2000)
        }
    }
}
</script>