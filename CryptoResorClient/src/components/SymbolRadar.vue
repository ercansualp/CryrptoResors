<template>
  <DataTable sortField="time" :sortOrder="-1" :value="symbolRadarDatas" tableStyle="width: 37rem;" v-on:row-click="onRowClick" v-bind:rowStyle="rowStyle">
    <Column v-bind:sortable="column.sortable" v-bind:field="column.field" v-bind:header="column.title" v-for="column in symbolRadarColumns"></Column>
  </DataTable>
</template>

<script lang="ts">
import DataTable from "primevue/datatable";
import Column from "primevue/column";
import 'primevue/resources/themes/aura-light-green/theme.css'
import {HubConnectionBuilder} from "@microsoft/signalr";
import axios from "axios";

export default {
  name: 'symbol-radar',
  components: {
    DataTable,
    Column
  },
  data() {
    return {
      connection: null,
      symbolRadarDatas: [],
      symbolRadarColumns: [
        {
          title: 'Symbol',
          field: 'name',
          filter: true,
          search: true,

        },
        {
          title: 'Change (%)',
          field: 'change',
          type: 'number',
        },
        {
          title: 'Time',
          field: 'time',
          value: this.formatDateTime,
          sortable: true
        }
      ],
      coins: [
          "BTCUSDT", "ETHFIUSDT", "APTUSDT", "XAIUSDT", "RDNTUSDT", "ARBUSDT", "ETHUSDT", "BNBUSDT", "SOLUSDT", "USDCUSDT", "XRPUSDT"
      ]
    }
  },
  mounted() {
    // SignalR bağlantısını oluştur
    this.connection = new HubConnectionBuilder()
        .withUrl("https://localhost:7021/notification") // SignalR hub URL'sini buraya ekleyin
        .build();
    // Bağlantıyı başlat
    this.connection.start()
        .then(async () => {
          console.log("SignalR bağlantısı başarıyla başlatıldı.");
          for(let i = 0; i < this.coins.length; i++)
            await axios.get(`https://localhost:7021/api/CryrptoResorsBase/${this.coins[i]}`);

          // Hub üzerinden gelen mesajları dinle
          this.connection.on("ReceiveMessage", _radarSymbol => {
            console.log(_radarSymbol)
            var index = -1;
            this.symbolRadarDatas.map((_symbol, _index) => {
              if(_symbol.name === _radarSymbol.name)
                index = _index;
            });
            if(index === -1)
              this.symbolRadarDatas.push(_radarSymbol);
            else
              this.symbolRadarDatas[index] = _radarSymbol;
          });
        })
        .catch(error => {
          console.error("SignalR bağlantısı başlatılırken hata oluştu: ", error);
        });
  },
  methods: {
    onRowClick(event) {
      this.$emit("changeCurrentSymbol", {symbol: event.data.name})
    },
    rowStyle: (data) => {
      if (data.change <= -0.1) {
        return { color: 'red' };
      }
      else if (data.change >= 0.1) {
        return { color: 'green' };
      }
    },
    formatDateTime(data) {
      console.log("ahmet: ", data);
      const date = new Date(data.time);
      return `${date.getMonth() + 1}/${date.getDate()}/${date.getFullYear()} ${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}`;
    },
  }
}
</script>

<style scoped>

</style>
