<template>
    <div class="manage-table">
        <h1 class="manage-table__title">
            Manage Cars
        </h1>

        <div class="table-functions">
            <div class="search-container">
                <input class="search-container__input" type="text" placeholder="Find Product">
                <button class="search-container__btn">Find</button>
            </div>
            <button
                class="add-new-btn"
                @click.prevent="openManageModal"
            >
                Add New Car
            </button>
        </div>

        <table class="manage-table__table" border aria-label="manage cars table">

            <thead class="manage-table__table--head">
                <tr>
                    <th scope="col" class=""></th>
                    <th scope="col" class="">Car Id</th>
                    <th scope="col" class="">Car Name</th>
                    <th scope="col" class="">Car Model</th>
                    <th scope="col" class="">Car Year</th>
                    <th scope="col" class="">Car Price</th>
                    <th scope="col" class="">Actiuni</th>
                </tr>
            </thead>

            <tbody class="manage-table__table--body">
                <tr>
                    <td>1</td>
                    <td>1</td>
                    <td>Audi</td>
                    <td>A4</td>
                    <td>2022</td>
                    <td>$36.000</td>
                    <td>
                        Delete
                    </td>
                </tr>
                <tr>
                    <td>2</td>
                    <td>4</td>
                    <td>BMW Limited Edition Space</td>
                    <td>320D xDrive M Packet Full Option M Packet Full Option M Packet Full Option M Packet Full Option</td>
                    <td>2022 month 11</td>
                    <td>$110.000</td>
                    <td>
                        <select class="manage-item" v-model="selectedOption" @change="toggleManageModal">
                            <option value="" disabled>Select option</option>
                            <option value="update">Update</option>
                            <option value="delete">Delete</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>3</td>
                    <td>7</td>
                    <td>Audi</td>
                    <td>A8</td>
                    <td>2023</td>
                    <td>$86.000</td>
                    <td>
                        Delete
                    </td>
                </tr>
            </tbody>
        </table>

        <div class="pagination">
            <div class="pagination__info">
                <p>
                    Showing
                    <!-- <span> startListCount  </span> -->
                    <span> 1  </span>
                    -
                    <!-- <span> endListCount mai mic  totalCount ? endListCount : totalCount  </span> -->
                    <span> 5  </span>
                    of
                    <!-- <span> totalCount  </span> -->
                    <span> 20  </span>
                    results
                </p>
            </div>

            <div class="pagination__controls">
                <button
                    disabled="{prevBtnDisabled}"
                    class="click prev page"
                >
                    Prev
                </button>
                <button
                    disabled="{nextBtnDisabled}"
                    class="click next page"
                >
                    Next
                </button>
            </div>
        </div>
    </div>

    <manage-modal :manageModalTitle="manageModalTitle" />
</template>

<script>
import { mapStores, mapState, mapWritableState } from 'pinia';
import useManageModalStore from '@/stores/manageModal';
import ManageModal from './ManageModal.vue';

export default {
    name: 'ManageTable',
    data() {
        return {
            x: 2,
            selectedOption: '',
            manageModalTitle: ''
        }
    },
    components: {
        ManageModal
    },
    computed: {
        ...mapStores(useManageModalStore),
        ...mapState(useManageModalStore, ['hiddenClass']),
    },
    methods: {
        toggleManageModal() {
            this.manageModalStore.isOpen = !this.manageModalStore.isOpen;
            this.manageModalStore.modalType = this.selectedOption;
            console.log(this.manageModalStore.isOpen ? 'manage modal open' : 'manage modal closed');

            console.log(this.manageModalStore.modalType);
            this.manageModalTitle = this.manageModalStore.modalType + ' item';

          this.selectedOption = '';
        },
        openManageModal() {
            this.manageModalStore.isOpen = !this.manageModalStore.isOpen;
            this.manageModalStore.modalType = 'add';
            this.manageModalTitle = this.manageModalStore.modalType + ' item';

            console.log(this.manageModalTitle)
        }
    }
}
</script>