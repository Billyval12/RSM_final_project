<template>
  <table v-if="state.products && state.products.length > 0" class="custom-table">
    <!-- Encabezado de la tabla -->
    <thead>
      <tr>
        <th>Id</th>
        <th>Product Name</th>
        <th>Category</th>
        <th>Total Sales</th>
        <th>% of Total Sales</th>
        <th>% of Total Category Sales</th>
      </tr>
    </thead>
    <!-- Cuerpo de la tabla -->
    <tbody>
      <tr v-for="product in paginatedProducts" :key="product.id">
        <td>{{ product.id }}</td>
        <td>{{ product.productName }}</td>
        <td>{{ product.productCategory }}</td>
        <td>{{ product.totalSales }}</td>
        <td>{{ product.percentageOfTotalSalesInRegion }}</td>
        <td>{{ product.percentageOfTotalCategorySalesInRegion }}</td>
      </tr>
    </tbody>
    <!-- Pie de la tabla (paginación) -->
    <tfoot>
      <tr>
        <td colspan="6">
          <div class="pagination">
            <button
              @click="goToPage(state.currentPage - 1)"
              :disabled="state.currentPage === 1"
              class="pagination-button"
            >
              &lt;
            </button>
            <span
              v-for="page in visiblePages"
              :key="page"
              @click="goToPage(page)"
              :class="{ active: page === state.currentPage }"
              class="page-number"
            >
              {{ page }}
            </span>
            <button
              @click="goToPage(state.currentPage + 1)"
              :disabled="state.currentPage === totalPages"
              class="pagination-button"
            >
              &gt;
            </button>
          </div>
        </td>
      </tr>
    </tfoot>
  </table>
  <table v-else class="custom-table empty-table">
    <thead>
      <tr>
        <th>Id</th>
        <th>Product Name</th>
        <th>Category</th>
        <th>Total Sales</th>
        <th>% of Total Sales</th>
        <th>% of Total Category Sales</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td colspan="6">No results found.</td>
      </tr>
    </tbody>
  </table>
</template>

<script setup>
  // Importaciones y definiciones de datos
  import { computed, reactive, defineProps } from "vue";

  const props = defineProps({
    products: Array,
  });

  const state = reactive({
    products: computed(() => props.products),
    pageSize: 20,
    currentPage: 1,
  });

  const paginatedProducts = computed(() => {
    const startIndex = (state.currentPage - 1) * state.pageSize;
    const endIndex = startIndex + state.pageSize;
    return state.products.slice(startIndex, endIndex);
  });

  const totalPages = computed(() =>
    Math.ceil(state.products.length / state.pageSize)
  );

  const visiblePages = computed(() => {
    const range = 4;
    const start = Math.max(1, state.currentPage - range);
    const end = Math.min(totalPages.value, start + range * 2);

    const result = [];
    for (let i = start; i <= end; i++) {
      result.push(i);
    }
    return result;
  });

  // Función para cambiar de página
  function goToPage(page) {
    state.currentPage = page;
  }
</script>

<style>
  /* Estilos de la tabla */
  .custom-table {
    width: 100%;
    border-collapse: collapse;
    border-radius: 8px;
    overflow: hidden;
    margin-top: 20px;
    font-family: 'Poppins', sans-serif; /* Utilizar la fuente Poppins */
  }
  
  .custom-table th,
  .custom-table td {
    padding: 12px;
    text-align: left;
    border-bottom: 1px solid #ddd;
  }
  
  .custom-table th {
    background-color: #f2f2f2;
  }
  
  .custom-table tfoot {
    background-color: #f2f2f2;
  }
  
  /* Estilos de la paginación */
  .pagination {
    margin-top: 10px;
    display: flex;
    justify-content: center;
    align-items: center;
  }
  
  .pagination-button {
    cursor: pointer;
    padding: 8px 12px;
    border: none;
    background-color: #f2f2f2;
    transition: background-color 0.3s ease;
  }
  
  .pagination-button:hover {
    background-color: #ddd;
  }
  
  .page-number {
    padding: 8px 12px;
    cursor: pointer;
  }
  
  .page-number.active {
    background-color: #181723;
    color: #fff;
    border-radius: 4px;
  }
  
  /* Estilos para tabla vacía */
  .empty-table {
    margin-top: 20px;
  }
</style>


