import { createRouter, createWebHistory } from 'vue-router';
import Home from '/src/pages/HomePage.vue';
//import Persons from '/src/pages/PersonsPage.vue';
//import Products from '/src/pages/ProductsPage.vue';
import SalesReportPage from './pages/SalesReportPage.vue';
import Sales from '/src/pages/NewProductsPage.vue';
import ReportPage from '/src/pages/ReportPage.vue';

const routes = [
  //{ path: '/persons', component: Persons },
  //{ path: '/products', component: Products },
  { path: '/', component: Home},
  { path: '/sales-report', component: SalesReportPage},
  { path: '/sales', component: Sales},
  { path: '/report', component: ReportPage}
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;