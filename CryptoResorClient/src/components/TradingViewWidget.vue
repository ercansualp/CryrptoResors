<template>
  <div class="tradingview-widget-container" style="height:100%;width:100%">
  </div>
</template>

<script lang="ts">
import { Chart } from 'vue-tradingview-widgets';

export default {
  name: "tradingview-widget",
  components: {
    Chart
  },
  props: {
    currentSymbol: String
  },
  mounted(){
    this.$watch('currentSymbol', (newValue, oldValue) => {
      this.updateWidget();
    });
    this.updateWidget();
  },
  methods: {
    updateWidget(){
      const widgetPlaceholder = document.getElementsByClassName('tradingview-widget-container')[0];
      widgetPlaceholder.replaceChildren(); // empty placeholder
      const script = document.createElement('script');
      script.src = 'https://s3.tradingview.com/external-embedding/embed-widget-advanced-chart.js'
      script.async = true;
      script.innerHTML = JSON.stringify({
        "autosize": true,
        "symbol": this.currentSymbol,
        "interval": "1",
        "timezone": "Europe/Istanbul",
        "theme": "dark",
        "style": "1",
        "locale": "en",
        "enable_publishing": false,
        "allow_symbol_change": true,
        "calendar": false,
        "support_host": "https://www.tradingview.com",
      });
      widgetPlaceholder.appendChild(script);
    }
  }
}
</script>

<style scoped>

</style>
