<template>
    <div class="manage-item-alert-message" v-if="addShowAlert" :class="addAlertVariant">
        {{ addAlertMessage }}
    </div>
    <vee-form class="update-form" @submit="updateItem" :validation-schema="carItemSchema">
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
export default {
    name: 'UpdateItem',
    data() {
        return {
            carItemSchema: {
                carName: "required|min:3|max:100",
                carModel: "required|min:3|max:100",
                carYear: "required|min_value:2015|max_value:2024",
                carPrice: "required|min_value:5000|max_value:5000000",
            },
            updateInSubmission: false,
            updateShowAlert: false,
            updateAlertVariant: 'bg-blue-500',
            updateAlertMessage: 'Please wait. Processing...',
        }
    },
    methods: {
        updateItem(values) {
            console.log('update item');
            console.log(values);
        }
    }
}
</script>