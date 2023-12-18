<template>
    <div class="manage-item-alert-message" v-if="updateShowAlert" :class="updateAlertVariant">
        {{ updateAlertMessage }}
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
export default {
    name: 'AddItem',
    data() {
        return {
            carItemSchema: {
                carName: "required|min:3|max:100",
                carModel: "required|min:3|max:100",
                carYear: "required|min_value:2015|max_value:2024",
                carPrice: "required|min_value:5000|max_value:5000000",
            },
            addInSubmission: false,
            addShowAlert: false,
            addAlertVariant: 'bg-blue-500',
            addAlertMessage: 'Please wait. Processing...',
        }
    },
    methods: {
        addItem(values) {
            console.log('add item');
            console.log(values);
        }
    }
}
</script>