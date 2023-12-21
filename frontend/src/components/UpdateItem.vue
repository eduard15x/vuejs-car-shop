<template>
    <div class="manage-item-alert-message" v-if="updateShowAlert" :class="updateAlertVariant">
        {{ updateAlertMessage }}
    </div>
    <vee-form class="update-form" @submit="updateItem" :validation-schema="carItemSchema" :initial-values="selectedItemFromList">
        <div class="update-form__field">
            <label>Car Name</label>
            <vee-field
                type="text"
                class="update-form__field--input"
                placeholder="Name"
                name="carName"
            />
            <ErrorMessage class="error" name="carName" />
        </div>
        <div class="update-form__field">
            <label>Car Model</label>
            <vee-field
                type="text"
                class="update-form__field--input"
                placeholder="Model"
                name="carModel"
            />
            <ErrorMessage class="error" name="carModel" />
        </div>
        <div class="update-form__field">
            <label>Car Year</label>
            <vee-field
                type="number"
                class="update-form__field--input"
                placeholder="Year"
                name="carYear"
            />
            <ErrorMessage class="error" name="carYear" />
        </div>
        <div class="update-form__field">
            <label>Car Price</label>
            <vee-field
                type="number"
                class="update-form__field--input"
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
            :disabled="updateInSubmission"
            type="submit"
            class="update-form__submit-btn"
        >
            Update
        </button>
    </vee-form>
</template>

<script>
import { mapStores } from 'pinia';
import useUserStore from '@/stores/user';
import useManageModalStore from '@/stores/manageModal';

export default {
    name: 'UpdateItem',
    props: {
        fetchItems: {
            type: Function,
            required: true
        },
        action: {
            type: Function,
            required: true
        },
        selectedItemFromList: {
            type: Object,
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
            updateInSubmission: false,
            updateShowAlert: false,
            updateAlertVariant: 'loading-variant',
            updateAlertMessage: 'Please wait. Processing...',
        }
    },
    computed: {
        ...mapStores(useUserStore, useManageModalStore),
    },
    methods: {
        async updateItem(values) {
            this.updateInSubmission = true;
            this.updateShowAlert = true;
            this.updateAlertVariant = 'loading-variant';
            this.updateAlertMessage = 'Please wait. Processing...';

            values.userId = this.userStore.userId ? this.userStore.userId : 0;

            try {
                const response = await fetch(`https://localhost:7090/api/cars/update/${this.selectedItemFromList.id}`, {
                    method: 'PUT',
                    headers: {"Content-Type": "application/json"},
                    credentials: 'include',
                    body: JSON.stringify(values)
                });
                const json = await response.json();

                if (json.statusCode !== 200) {
                    this.updateInSubmission = false;
                    this.updateAlertVariant = 'error-variant';
                    this.updateAlertMessage = json.value;
                    return;
                }

            } catch (error) {
                console.error(error);
                this.updateInSubmission = false;
                this.updateAlertVariant = 'error-variant';
                this.updateAlertMessage = error;
                return;
            }

            this.updateAlertVariant = 'success-variant';
            this.updateAlertMessage = 'Product updated successfully.';
            await this.action();

            setTimeout(() => {
                this.updateInSubmission = false;
                this.updateShowAlert = false;
                this.manageModalStore.isOpen = !this.manageModalStore.isOpen;
            }, 2000)
        }
    }
}
</script>