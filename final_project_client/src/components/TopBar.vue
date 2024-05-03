<template>
  <div class="top-bar">
    <div class="logo">Adventure Works</div>
    <div class="nav-links">
      <router-link to="/" class="nav-link">
        <div class="icon-container">
          <i class="material-icons">home</i>
          <span>Home</span>
        </div>
      </router-link>
      <router-link to="/sales-report" class="nav-link">
        <div class="icon-container">
          <i class="material-icons">bar_chart</i>
          <span>Products Report</span>
        </div>
      </router-link>
      <router-link to="/sales" class="nav-link">
        <div class="icon-container">
          <i class="material-icons">attach_money</i>
          <span>Sales Report</span>
        </div>
      </router-link>
    </div>
  </div>
</template>

<script setup></script>

<style scoped>
  .top-bar {
    background-color: #2c3e50; 
    color: #fff; 
    padding: 15px;
    position: fixed;
    width: 100%;
    top: 0;
    display: flex;
    justify-content: space-between;
    align-items: center;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    font-weight: bold;
  }

  .logo {
    margin-left: 20px;
    font-size: 20px;
  }

  .nav-links {
    display: flex;
    margin-right: 20px; 
  }

  .nav-link {
    text-decoration: none;
    color: #fff;
    display: flex;
    align-items: center;
    margin-left: 20px;
    transition: color 0.3s ease;
  }

  .nav-link:hover {
    color: #3498db;
  }

  .icon-container {
    display: flex;
    align-items: center;
  }

  .material-icons {
    font-size: 24px;
    margin-right: 8px;
  }
</style>




