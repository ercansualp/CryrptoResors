<template>
<div class="min-h-screen flex items-center justify-center">
  <div class="flex w-full px-10 gap-x-4">
    <div class="flex flex-col border border-[#ddd] rounded-md p-2 w-[350px]">
      <symbol-table v-on:changeCurrentSymbol="changeCurrentSymbol($event)"></symbol-table>
    </div>
    <div class="grow min-h-[400px]">
      <trading-view-widget v-bind:currentSymbol="currentSymbol"></trading-view-widget>
    </div>
    <div class="border border-[#ddd] rounded-md">
      <symbol-radar v-on:changeCurrentSymbol="changeCurrentSymbol($event)"></symbol-radar>
    </div>
  </div>
</div>
</template>

<script lang="ts">
import SymbolRadar from './components/SymbolRadar.vue';
import SymbolTable from './components/SymbolTable.vue';
import TradingViewWidget from "./components/TradingViewWidget.vue";

export default {
  name: 'App',
  components: {
    SymbolRadar,
    SymbolTable,
    TradingViewWidget
  },
  data(){
    return {
      currentSymbol: "NASDAQ:AAPL",
    }
  },
  methods: {
    changeCurrentSymbol(symbol){
      this.currentSymbol = symbol.symbol;
    }
  }
};
</script>

<style scoped>
</style>
