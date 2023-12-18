<template>
    <div class="manage-modal" :class="hiddenClass">
        <div class="manage-modal__content">
            <h2 class="manage-modal__content--title">
                {{ manageModalTitle }}
            </h2>

            <div class="manage-modal__content--close-btn" @click.prevent="modalVisibility = false">
                <i class="fas fa-times"></i>
            </div>

            <add-item v-if="modalType === 'add'" />
            <update-item v-else-if="modalType === 'update'" />
            <delete-item v-else />
        </div>
    </div>
</template>

<script>
import { mapStores, mapState, mapWritableState } from 'pinia';
import useManageModalStore from '@/stores/manageModal';
import AddItem from './AddItem.vue';
import UpdateItem from './UpdateItem.vue';
import DeleteItem from './DeleteItem.vue';


export default {
    name: 'ManageModal',
    props: {
        manageModalTitle: {
            type: String,
            required: true
        }
    },
    computed: {
        ...mapStores(useManageModalStore),
        ...mapState(useManageModalStore, ['hiddenClass']),
        ...mapWritableState(useManageModalStore, {
            modalVisibility: 'isOpen',
            modalType: 'modalType'
        }),
    },
    components: {
        AddItem,
        UpdateItem,
        DeleteItem
    }
}
</script>