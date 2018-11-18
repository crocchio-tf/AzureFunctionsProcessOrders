<template>
<div class="orders-page">
  <h1>Orders</h1>
  <order-list :orders="orders"></order-list>
</div>
</template>

<script>
import axios from 'axios';
import sortBy from 'lodash/sortBy';
import OrderList from './OrderList.vue';
import * as signalR from '@aspnet/signalr';

export default {
    name: "app",
    data: () => ({
        orders: [],
    }),
    components: {
      'order-list': OrderList,
    },
    methods: {
      loadOrders: function() {
        return axios
          .get('http://localhost:7071/api/GetOrders')
          .then(response => this.orders = sortBy(response.data, '_ts'));
      },

      registerHubConnection: function() {
        return axios
          .get('http://localhost:7071/api/GetSignalRConnectionInfo')
          .then(response => {
            const info = response.data;

            let options = {
              accessTokenFactory: () => info.accessToken
            };

            this.hubConnection = new signalR.HubConnectionBuilder()
              .withUrl(info.url, options)
              .build();

            this.hubConnection.start().catch(err => console.error(err.toString()));

            this.hubConnection.on('broadcastOrder', order => {
              this.orders.push(order);
            });            
          });
      },
    },
    mounted () {
      this.loadOrders()
      this.registerHubConnection();
    }
};
</script>

<style>
.orders-page {
  max-width: 800px;
  margin: 0 auto;
}
</style>