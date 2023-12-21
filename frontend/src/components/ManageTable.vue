<template>
    <div class="manage-table">
        <h1 class="manage-table__title">
            Manage Cars
        </h1>

        <div class="table-functions">
            <div class="search-container">
                <input class="search-container__input" type="text" placeholder="Find Car" :value="searchTableInput" @change="e => searchTableInput = e.target.value">
                <button class="search-container__btn" @click.prevent="fetchItems">Search</button>
            </div>
            <button
                class="add-new-btn"
                @click.prevent="openManageModal"
            >
                Add Car
            </button>
        </div>

        <div class="table-wrapper">
            <table class="manage-table__table" border aria-label="manage cars table">
                <thead class="manage-table__table--head">
                    <tr>
                        <th v-if="carList.length !== 0" scope="col" class="">Nr</th>
                        <th scope="col" class="">Id</th>
                        <th scope="col" class="">Car Name</th>
                        <th scope="col" class="">Model</th>
                        <th scope="col" class="">Engine</th>
                        <th scope="col" class="">Year</th>
                        <th scope="col" class="">Car Price</th>
                        <th scope="col" class="">Actiuni</th>
                    </tr>
                </thead>

                <tbody v-if="carList.length !== 0" class="manage-table__table--body">
                    <tr v-for="(car, index) in carList" :key="index">
                        <td>{{ index + 1 + itemNumberInTable }}</td>
                        <td>{{ car.id }}</td>
                        <td>{{ car.carName }}</td>
                        <td>{{ car.carModel }}</td>
                        <td>{{ car.engine }}</td>
                        <td>{{ car.carYear }}</td>
                        <td>${{ car.carPrice }}</td>
                        <td>
                            <select class="manage-item" :data-car-id="car.id" v-model="selectedOption" @change="toggleManageModal">
                                <option value="" disabled>Select option</option>
                                <option value="update">Update</option>
                                <option value="delete">Delete</option>
                            </select>
                        </td>
                    </tr>
                </tbody>

                <tbody v-else class="empty-table">
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td class="text">No items available</td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </tbody>
            </table>
        </div>
        

        <table-pagination
            v-if="showPagination"
            :currentPage="currentPage"
            :pageSize="pageSize"
            :totalCount="totalCarsCount"
            :handlePageChange="handlePageChange"
        />
    </div>

    <manage-modal
        :manageModalTitle="manageModalTitle"
        :fetchItems="fetchItems"
        :addItem="addItem"
        :updateItem="updateItem"
        :deleteItem="deleteItem"
        :selectedItemFromList="selectedItemFromList"
    />
</template>

<script>
import { mapStores, mapState } from 'pinia';
import useUserStore from '@/stores/user';
import useManageModalStore from '@/stores/manageModal';
import ManageModal from './ManageModal.vue';
import TablePagination from './TablePagination.vue';

export default {
    name: 'ManageTable',
    data() {
        return {
            selectedOption: '',
            manageModalTitle: '',
            carList: [],
            currentPage: 1,
            totalCarsCount: 0,
            pageSize: 5,
            showPagination: false,
            itemNumberInTable: 0,
            searchTableInput: '',
            selectedItemFromList: {}
        }
    },
    components: {
        ManageModal,
        TablePagination
    },
    computed: {
        ...mapStores(useUserStore, useManageModalStore),
        ...mapState(useManageModalStore, ['hiddenClass']),
    },
    methods: {
        toggleManageModal($event) {
            const carId = Number($event.target.dataset.carId);

            this.manageModalStore.isOpen = !this.manageModalStore.isOpen;
            this.manageModalStore.modalType = this.selectedOption;

            this.selectedItemFromList = this.carList.find((car) => car.id === carId);
            this.manageModalTitle = this.manageModalStore.modalType + ' item';
            this.selectedOption = '';
        },
        openManageModal() {
            this.manageModalStore.isOpen = !this.manageModalStore.isOpen;
            this.manageModalStore.modalType = 'add';
            this.manageModalTitle = this.manageModalStore.modalType + ' item';
        },
        handlePageChange(pageNumber) {
            this.currentPage = pageNumber;
            this.fetchItems();
        },
        async fetchItems() {
            try {
                const response = await fetch(`https://localhost:7090/api/cars/user/${this.userStore.userId}?page=${this.currentPage}&pageSize=${this.pageSize}&search=${this.searchTableInput}`, {
                    method: 'GET',
                    credentials: 'include',
                });

                const json = await response.json();
                const totalCarsNr = json.value.totalCars;

                if (totalCarsNr > 0) {
                    if (totalCarsNr <= this.pageSize) {
                        this.showPagination = false;
                    } else {
                        this.showPagination = true;
                    }

                    this.carList = json.value.carList;
                    this.totalCarsCount = totalCarsNr;
                    this.itemNumberInTable = this.currentPage * this.pageSize - this.pageSize;
                }
            } catch(error) {
                this.carList = [];
            }
            console.log(this.carList);
        },
        async addItem() {
            this.fetchItems();
        },
        async updateItem() {
            this.fetchItems();

        },
        async deleteItem() {
            this.fetchItems();
        }
    },
    created() {
        this.fetchItems();
    }
}
</script>