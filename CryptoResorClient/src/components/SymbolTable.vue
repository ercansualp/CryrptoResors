 <template>
  <div class="card">
    <div class="flex justify-content-center align-items-center mb-4 gap-2">
      <label for="search" class="p-mr-2">Coin List</label>
      <InputText id="search" v-model="searchText" placeholder="Search Symbol" />
    </div>
    <DataTable :value="filteredSymbolDatas" :rows="10" :paginator="true" paginatorPosition="bottom" selectionMode="single" dataKey="id" tableStyle="min-width: 30rem" @row-click="changeCurrentSymbol">
      <Column field="symbol" header="Symbol"></Column>
      <Column field="price" header="Price"></Column>
    </DataTable>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, computed, onMounted, inject } from 'vue';
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import InputText from 'primevue/inputtext';
import axios from 'axios';

export default defineComponent({
  name: "symbol-table",
  components: {
    DataTable,
    Column,
    InputText
  },
  setup() {
    const changeCurrentSymbol2 = inject('changeCurrentSymbol');
    const symbolDatas = ref([]);
    const searchText = ref('');
    const selectedProduct = ref(null);

    const fetchData = () => {
      axios.get("https://api.binance.com/api/v3/ticker/price")
        .then(response => {
          const filteredData = response.data.filter((coin: { symbol: string }) => coin.symbol.endsWith("USDT"));
          symbolDatas.value = filteredData;
        })
        .catch(error => {
          console.error('Error fetching data:', error);
        });
    };

    onMounted(() => {
      fetchData();
    });

    const filteredSymbolDatas = computed(() => {
      return symbolDatas.value.filter((coin: { symbol: string }) => coin.symbol.toLowerCase().includes(searchText.value.toLowerCase()));
    });

    function changeCurrentSymbol(rowData) {
      changeCurrentSymbol2({symbol: rowData.data.symbol});
      //this.$emit("changeCurrentSymbol", { symbol: rowData.data.symbol });
    }

    return {
      searchText,
      filteredSymbolDatas,
      selectedProduct,
      changeCurrentSymbol
    };
  }
});
</script>

<style scoped>
/* Sembollerin üzerine gelindiğinde imleci değiştir */
.p-datatable tbody tr:hover {
  cursor: pointer;
}

/* Tıklanabilir sembollere hover efekti ekle */
.p-datatable tbody tr:hover td {
  background-color: #f0f0f0; /* Opsiyonel: Hover efekti için arkaplan rengi değiştirme */
}
</style> 


