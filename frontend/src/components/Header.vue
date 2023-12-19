<template>
    <header id="header" class="header">
        <nav class="header__nav">
            <router-link
                class="header__nav--home-link"
                :to="{ name: 'home'}"
                exact-active-class="no-active"
            >
                VueCARS
            </router-link>

            <div class="header__nav--primary">
                <ul class="header__nav-list">
                    <li>
                        <router-link
                            class="primary-link"
                            :to="{ name: 'home'}"
                        >
                            About
                        </router-link>
                    </li>
                    <li v-if="!userStore.isLoggedIn">
                        <router-link
                            class="primary-link"
                            :to="{ name: 'home'}"
                            @click.prevent="toggleAuthModal"
                        >
                            Login / Register
                        </router-link>
                    </li>
                    <template v-else>
                        <li>
                            <router-link class="primary-link" :to="{ name: 'manage' }">Manage</router-link>
                        </li>
                        <li>
                            <a class="primary-link" href="#" @click.prevent="signOut">Logout</a>
                        </li>
                    </template>
                </ul>
                <li class="account-email" v-if="userStore.isLoggedIn">
                    {{ userStore.email }}
                </li>
            </div>
        </nav>
    </header>
</template>

<script>
import { mapStores, mapActions } from 'pinia';
import useAuthModalStore from '@/stores/authModal';
import useUserStore from '@/stores/user';

export default {
    name: 'AppHeader',
    computed: {
        ...mapStores(useAuthModalStore, useUserStore),
        // ...mapWritableState(useModalStore, ['isOpen']),
    },
    methods: {
        ...mapActions(useUserStore, ['logout']),
        toggleAuthModal() {
            this.authModalStore.isOpen = !this.authModalStore.isOpen;
            // console.log(this.authModalStore.isOpen ? 'modal open' : 'modal closed');
        },
        async signOut() {
            await this.logout();

            if (this.$route.meta.requiresAuth) {
                this.$router.push({ name: 'home' }); //
            }
        }
    }
}
</script>