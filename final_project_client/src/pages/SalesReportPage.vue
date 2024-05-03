<template>
  <div class="sales-report-page">
    <loader-comp :loading="state.loading" />
    <SearchBox @searchByOrderID="searchByProductID"
               @searchByName="searchByProductName" 
               @searchByCategory="searchByCategory" 
    />
    <sales-table :sales="state.sales" />
  </div>
</template>

<script setup>
import { onMounted, reactive } from "vue";
import SalesTable from "@/components/SalesReportTable.vue";
import axios from "/src/utils/axios.js";
import LoaderComp from "@/components/LoaderComp.vue";
import SearchBox from "@/components/SearchBox.vue"; 

const state = reactive({
  loading: false,
  sales: [],
});

async function fetchData() {
  try {
    state.loading = true;
    const response = await axios.get("/api/SalesReport/GetSalesReport");
    state.sales = response.data;
  } catch (error) {
    console.error(error);
  } finally {
    state.loading = false;
  }
}

async function searchByProductName(productName) {
  try {
    state.loading = true;
    const response = await axios.get(`/api/SalesReport/GetSalesReport?productName=${productName}`);
    state.sales = response.data;
  } catch (error) {
    console.error(error);
  } finally {
    state.loading = false;
  }
}

async function searchByProductID(productID) {
  try {
    state.loading = true;
    const response = await axios.get(`/api/SalesReport/GetSalesReport?productID=${productID}`);
    state.sales = response.data;
  } catch (error) {
    console.error(error);
  } finally {
    state.loading = false;
  }
}

async function searchByCategory(category) {
  try {
    state.loading = true;
    const response = await axios.get(`/api/SalesReport/GetSalesReport?productCategory=${category}`);
    state.sales = response.data;
  } catch (error) {
    console.error(error);
  } finally {
    state.loading = false;
  }
}

onMounted(async () => {
  await fetchData();
});
</script>

<style scoped>
.sales-report-page {
  width: 75%;
  margin-top: 80px;
  margin-bottom: 50px;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}
</style>

