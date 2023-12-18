<template>
    <div class="authentication" :class="!hiddenClass">
        <div class="modal">

            <div class="modal__content">
                <h2 class="modal__content--title">Your Account</h2>
                <p class="modal__content--close-btn" @click.prevent="modalVisibility = false">X</p>

                <ul class="modal__content--tabs">
                    <li
                        :class="{ 'active-element': tab === 'login' }"
                        @click.prevent="tab = 'login'"
                    >
                        <a href="">
                            Login
                        </a>
                    </li>
                    <li
                        :class="{ 'active-element':  tab === 'register' }"
                        @click.prevent="tab = 'register'"
                    >
                        <a href="">
                            Register
                        </a>
                    </li>
                </ul>

                <login-form v-if="tab === 'login'" />
                <register-form v-else />
            </div>
        </div>
    </div>
</template>

<script>
import { mapStores, mapState, mapWritableState } from 'pinia';
import useModalStore from '@/stores/modal';
import LoginForm from './LoginForm.vue';
import RegisterForm from './RegisterForm.vue';

export default {
    name: 'Authentication',
    data() {
        return {
            tab: 'login'
        }
    },
    components: {
        LoginForm,
        RegisterForm
    },
    computed: {
        ...mapStores(useModalStore),
        ...mapState(useModalStore, ['hiddenClass']),
        ...mapWritableState(useModalStore, {
            modalVisibility: 'isOpen'
        }),
    },
    // methods: {
    //     closeModal() {
    //         this.modalStore.isOpen = false;
    //     }
    // }
}
</script>