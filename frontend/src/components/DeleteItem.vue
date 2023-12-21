<template>
    <div class="delete-form">
        <p class="alert-message">
            Are you sure that you want to delete the item {{ selectedItemFromList.carName }} with the id {{ selectedItemFromList.id }} ?
        </p>
        <button
            :disabled="deleteInSubmission"
            type="submit"
            class="delete-btn"
            @click.prevent="deleteItem"
        >
            Submit
        </button>
    </div>
</template>

<script>
import { mapStores } from 'pinia';
import useManageModalStore from '@/stores/manageModal';

export default {
    name: 'DeleteItem',
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
            deleteInSubmission: false,
        }
    },
    computed: {
        ...mapStores(useManageModalStore),
    },
    methods: {
        async deleteItem() {
            this.deleteInSubmission = true;
            try {
                const response = await fetch(`https://localhost:7090/api/cars/delete/${this.selectedItemFromList.id}`, {
                    method: 'DELETE',
                    credentials: 'include'
                });
                const json = await response.json();
                console.log(json);
            } catch(error) {
                console.error(error);
            }

            await this.action();

            setTimeout(() => {
                this.deleteInSubmission = true;
                this.manageModalStore.isOpen = !this.manageModalStore.isOpen;
            }, 1000)
        }
    }
}
</script>