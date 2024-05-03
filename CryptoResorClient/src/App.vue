<template>
  <div class="min-h-screen flex items-center justify-center">
    <div class="flex w-full px-10 gap-x-4">
      <div class="flex flex-col border border-[#ddd] rounded-md p-2 w-[350px]">
        <SymbolTable @changeCurrentSymbol="changeCurrentSymbol"></SymbolTable> <!--soldaki tablo-->
      </div>
      <div class="grow min-h-[400px]">
        <TradingViewWidget :currentSymbol="currentSymbol"></TradingViewWidget> <!--widget-->
      </div>
      <div class="border border-[#ddd] rounded-md">
        <SymbolRadar @changeCurrentSymbol="changeCurrentSymbol"></SymbolRadar> <!--radar-->
      </div>
    </div>
    <Deneme></Deneme>
  </div>
</template>

<script lang="ts">
import { ref, provide } from 'vue';
import SymbolRadar from './components/SymbolRadar.vue';
import SymbolTable from './components/SymbolTable.vue';
import TradingViewWidget from "./components/TradingViewWidget.vue";
import Deneme from "./components/Deneme.vue";

export default {
  name: 'App',
  components: {
    SymbolRadar,
    SymbolTable,
    TradingViewWidget,
    Deneme
  },
  setup() {
    const currentSymbol = ref("NASDAQ:AAPL");

    const changeCurrentSymbol = (symbol) => {
      console.log("veri: ", symbol);
      currentSymbol.value = symbol.symbol;
    };

    provide('changeCurrentSymbol', changeCurrentSymbol);

    return {
      currentSymbol,
      changeCurrentSymbol
    };
  }
};
</script>

<style scoped>
</style>
