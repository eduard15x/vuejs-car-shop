import { defineStore } from "pinia";
import cookies from 'js-cookie'

export default defineStore('user', {
    state: () => ({
        email: '',
        userId: '',
        isLoggedIn: false,
    }),
    actions: {
        checkUser() {
            const userCookie = cookies.get('user');

            if (userCookie) {
                const userPropertiesArr = userCookie.split("-");

                this.userId = userPropertiesArr[0];
                this.email = userPropertiesArr[1];
                this.isLoggedIn = true;
            }
        },
        logout() {
            cookies.remove('token');
            cookies.remove('user');
            this.userId = '';
            this.email = '';
            this.isLoggedIn = false;

            // window.location.reload();
        }
    }
});