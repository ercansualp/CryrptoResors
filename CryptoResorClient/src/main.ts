import { createApp } from 'vue'
import App from './App.vue'
import './assets/css/tailwind.css'
import '@bhplugin/vue3-datatable/dist/style.css'
import PrimeVue from 'primevue/config';

const app = createApp(App);
app.use(PrimeVue);
app.mount('#app')
