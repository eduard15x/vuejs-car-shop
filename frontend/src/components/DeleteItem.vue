<template>
    <div class="delete-form">
        <p class="alert-message">
            Are you sure that you want to delete the item {item.name} with the id {item.id}
        </p>
        <button
            :disabled="loginInSubmission"
            type="submit"
            class="delete-btn"
            @click.prevent="deleteItem"
        >
            Submit
        </button>
    </div>
</template>

<script>
export default {
    name: 'DeleteItem',
    methods: {
        deleteItem() {
            console.log('delete car');
        }
    }
}
</script>