<template>
  <app-header />

  <router-view></router-view>

  <app-footer />

  <app-authentication />
</template>


<script>
import { mapActions } from 'pinia';
import useUserStore from '@/stores/user';
import AppHeader from '@/components/Header.vue';
import AppFooter from '@/components/Footer.vue';
import AppAuthentication from '@/components/Authentication.vue';


export default {
  name: 'App',
  components: {
    AppHeader,
    AppFooter,
    AppAuthentication
  },
  methods: {
      ...mapActions(useUserStore, ['checkUser']),
  },
  created() {
    this.checkUser();
  }
}
</script>
