<template>
  <div class="card">
    <div class="card-body">
      <div class="search-container">
        <div class="search-group">
          <label for="orderIDSearch" class="search-label">Order ID:</label>
          <input
            v-model="state.searchOrderID"
            @change="() => $emit('searchByOrderID', state.searchOrderID)"
            placeholder="Enter ID"
            class="search-input"
            id="orderIDSearch"
          />
        </div>
        <div class="search-group">
          <label for="nameSearch" class="search-label">Product Name:</label>
          <input
            v-model="state.searchText"
            @change="() => $emit('searchByName', state.searchText, state.selectedType)"
            placeholder="Enter name"
            class="search-input"
            id="nameSearch"
          />
        </div>
        <div class="search-group">
          <label for="categorySearch" class="search-label">Category:</label>
          <input
            v-model="state.searchCategory"
            @change="() => $emit('searchByCategory', state.searchCategory, state.selectedType)"
            placeholder="Enter category"
            class="search-input"
            id="categorySearch"
          />
        </div>
        <button @click="downloadPDF" class="pdf-button">
          <i class="material-icons pdf-icon">cloud_download</i> Download PDF
        </button>
        <button @click="exportCSV" class="csv-button">
          <i class="material-icons csv-icon">description</i> Export as CSV
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { reactive } from "vue";
import html2pdf from "html2pdf.js";

const state = reactive({
  searchText: "",
  searchCategory: "",
  selectedType: "",
  searchOrderID: "",
});

const downloadPDF = () => {
  const table = document.querySelector(".custom-table");
  const options = {
    margin: 1,
    filename: "table.pdf",
    image: { type: "jpeg", quality: 0.98 },
    html2canvas: { scale: 2 },
    jsPDF: { unit: "in", format: "letter", orientation: "portrait" },
  };

  html2pdf().from(table).set(options).save();
};


const exportCSV = () => {
  
  const table = document.querySelector(".custom-table");
  
  // Obtiene las filas de la tabla
  const rows = table.querySelectorAll("tr");
  
  // Inicializa una variable para almacenar el contenido del archivo CSV
  let csvContent = "data:text/csv;charset=utf-8,";

  // Itera sobre las filas de la tabla y agrega sus datos al contenido del CSV
  rows.forEach((row) => {
    const rowData = [];
    row.querySelectorAll("td").forEach((cell) => {
      rowData.push(cell.innerText);
    });
    csvContent += rowData.join(",") + "\n";
  });

  // Crea un enlace temporal y lo simula haciendo clic para descargar el archivo CSV
  const encodedUri = encodeURI(csvContent);
  const link = document.createElement("a");
  link.setAttribute("href", encodedUri);
  link.setAttribute("download", "table.csv");
  document.body.appendChild(link);
  link.click();
};

</script>

<style scoped>
.card {
  background-color: #f5f5f5;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.card-body {
  padding: 20px;
}

.search-container {
  display: flex;
  justify-content: center;
  align-items: center;
}

.search-group {
  margin-right: 20px;
}

.search-label {
  font-weight: bold;
  color: #333;
}

.search-input {
  font-size: 16px;
  font-family: "Roboto", sans-serif;
  padding: 10px;
  margin-right: 10px;
  border-radius: 4px;
  border: 1px solid #ddd;
  width: 200px;
}

.pdf-button,
.csv-button {
  font-size: 12px;
  font-family: "Roboto", sans-serif;
  padding: 6px 10px;
  border-radius: 4px;
  background-color: #2c3e50; 
  color: #fff;
  border: none;
  cursor: pointer;
  transition: background-color 0.3s;
  display: flex;
  align-items: center;
  margin-left: 10px;
}

.pdf-button:hover,
.csv-button:hover {
  background-color: #34495e;
}

.pdf-icon,
.csv-icon {
  font-size: 18px;
  margin-right: 5px;
}
</style>





