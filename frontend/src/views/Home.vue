<template>
  <div id="home" class="home">
    <home-banner :bannerInfo="bannerInfo" />
    <home-panel :services="services" />
  </div>
</template>

<script>
import bannerImg from '@/assets/banner.jpg';
import HomeBanner from '@/components/HomeBanner.vue';
import HomePanel from '@/components/HomePanel.vue';

export default {
  name: 'Home',
  components: {
    HomeBanner,
    HomePanel
  },
  data() {
    return {
      bannerInfo: {
        img: bannerImg,
        imgAlt: 'banner image'
      },
      services: [
        {
          title: 'Find the right car',
          description: 'There are cars for everyone.'
        },
        {
          title: 'Get the best offers',
          description: 'Best offers made by our approved dealers.'
        },
        {
          title: 'Contact our dealers',
          description: 'Our dealers will respond through our platform, email or phone.'
        }
      ]
    }
  }
}
</script>
