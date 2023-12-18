<template>
    <div class="home-panel">
        <div
            v-for="(service, index) in services"
            :key="index"
            class="home-panel__container"
        >
            <p class="service-number">{{ index + 1 }}</p>
            <h2 class="service-title">{{ service.title }}</h2>
            <p class="service-description">{{ service.description }}</p>
        </div>
    </div>
</template>

<script>
export default {
    name: 'HomePanel',
    props: {
        services: {
            type: Array,
            required: true
        }
    }
}
</script>