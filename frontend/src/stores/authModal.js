import { defineStore } from "pinia";

export default defineStore('authModal', {
    state: () => ({
        isOpen: false,
    }),
    getters: {
        hiddenClass(state) {
            return !state.isOpen ? 'hidden' : '';
        }
    }
});