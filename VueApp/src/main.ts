import { createApp } from 'vue'
import App from './App.vue'
import UsersPage from './components/UsersPage.vue';

const routes = [
	{ path: '/', component: UsersPage }
  ]
  
// const router = VueRouter.createRouter({
// 	history: VueRouter.createWebHashHistory(),
// 	routes, // short for `routes: routes`
//   })

createApp(App).mount('#app')
