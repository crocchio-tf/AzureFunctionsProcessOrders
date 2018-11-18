<template>
<div>
  <order v-for="(order, index) in orders" :key="index" :model="order" />
</div>
</template>

<script>
import Order from './Order.vue';

export default {
    name: 'order-list',
    props: ['orders'],
    components: {
      'order': Order,
    },
};
</script>