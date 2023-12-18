import { defineStore } from "pinia";

export default defineStore('manageModal', {
    state: () => ({
        isOpen: false,
        modalType: ''
    }),
    getters: {
        hiddenClass(state) {
            return !state.isOpen ? 'hidden' : '';
        }
    }
});