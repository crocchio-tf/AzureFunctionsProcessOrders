<template>
<div class="order">
  <b-card v-bind:class="{'not-viewed': !viewed, 'mb-1': true}">
    <div class="order-header-container">
      <div><strong>Order Confirmation #{{ model.confirmationNumber }}</strong></div>
      <b-btn v-on:click="handleClick" v-b-toggle="'order-' + model.confirmationNumber" variant="primary">
        <span class="order-open">Open</span>
        <span class="order-close">Close</span>
      </b-btn>
    </div>

    <b-collapse v-bind:id="'order-' + model.confirmationNumber">
      <b-card-body>
        <p class="card-text">
          <div><strong>Email</strong> {{ model.email }}</div>
          <div><strong>Items</strong></div>
          <b-list-group>
            <b-list-group-item v-for="(item, index) in model.items" :key="index">
              <b-container>
                <b-row>
                  <b-col cols="2"><strong>Name</strong></b-col>
                  <b-col>{{item.name}}</b-col>
                </b-row>
                <b-row>
                  <b-col cols="2"><strong>Quantity</strong></b-col>
                  <b-col>{{item.quantity}}</b-col>
                </b-row>
                <b-row>
                  <b-col><strong>Comments</strong></b-col>
                </b-row>
                <b-row>
                  <b-col>{{item.comments}}</b-col>
                </b-row>
              </b-container>
            </b-list-group-item>
          </b-list-group>
        </p>
      </b-card-body>
    </b-collapse>
    
  </b-card>
</div>
</template>

<script>
export default {
    name: 'order',
    props: ['model'],
    data: () => ({
      viewed: false,
    }),
    methods: {
      handleClick: function(e) {
        this.viewed = true;
      }
    }
};
</script>

<style>
  .order .not-viewed {
    background: #eee;
  }

  .order-header-container {
    display: flex;
    justify-content: space-between;
  }

  .order-header-container .collapsed .order-close,
  .order-header-container :not(.collapsed) .order-open {
    display: none;
  }
</style>