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
                    <li v-if=" 1 === 1">
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


            </div>
        </nav>
    </header>
</template>

<script>
import { mapStores } from 'pinia';
import useAuthModalStore from '@/stores/authModal';

export default {
    name: 'AppHeader',
    computed: {
        ...mapStores(useAuthModalStore),
        // ...mapWritableState(useModalStore, ['isOpen']),
    },
    methods: {
        toggleAuthModal() {
            this.authModalStore.isOpen = !this.authModalStore.isOpen;
            console.log(this.authModalStore.isOpen ? 'modal open' : 'modal closed');
            // this.isOpen = !this.isOpen;
            // console.log(this.isOpen ? 'modal open' : 'modal closed');
        },
        signOut() {
            console.log('signOut')
        }
    }
}
</script>