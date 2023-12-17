<template>
    <div class="home-banner">
        <div class="home-banner__container">
            <p class="banner-text">
                Buying a new car is such a big deal nowadays
                <br>
                <span>Find a new car deal</span>
            </p>
            <router-link
                class="banner-link"
                :to="{ name: 'home' }"
            >
                Find deals
            </router-link>
        </div>
        <img class="home-banner__img" :src="bannerInfo.img" alt="bannerInfo.imgAlt">
    </div>
</template>

<script>
export default {
    name: 'HomeBanner',
    props: {
        bannerInfo: {
            type: Object,
            required: true
        }
    },
}
</script>