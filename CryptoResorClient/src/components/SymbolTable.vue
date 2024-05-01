<template>
  <input type="text" class="border border-[#ddd] outline-none p-2 rounded-md" placeholder="Search a Symbol.." v-model="searchedSymbols">
  <vue3-datatable :rows="symbolDatas" :columns="symbolColumns" :search="searchedSymbols" v-on:rowClick="changeCurrentSymbol">
  </vue3-datatable>
</template>

<script lang="ts">
import Vue3Datatable from '@bhplugin/vue3-datatable'
import axios from 'axios';

export default {
  name: "symbol-table",
  components: {
    Vue3Datatable
  },
  data(){
    return {
      searchedSymbols: "",
      symbolColumns: [
        {
          title: 'Symbol',
          field: 'symbol',
          filter: true,
          search: true
        },
        {
          title: 'Price',
          field: 'price',
          type: 'number',
        }
      ],
      symbolDatas: []
    }
  },
  mounted(){
    axios.get("https://api.binance.com/api/v3/ticker/price").then((response) => {
      this.symbolDatas = response.data;
    });
  },
  methods: {
    changeCurrentSymbol(data){
      this.$emit("changeCurrentSymbol", {symbol: data.symbol});
    }
  }
}
</script>

<style scoped>

</style>
