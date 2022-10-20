import { createRouter, createWebHistory } from 'vue-router'
import UsersPage from './views/UsersPage.vue'
const routes = [
  {
    path: '/',
    name: 'Users',
    component: UsersPage
  },
  {
    path: '/create',
    name: 'Create',
    component: () => import('./views/CreateUserPage.vue')
  },
  {
    path: '/details/:id',
    name: 'Details',
    component: () => import('./views/DetailsPage.vue')
  },
  {
    path: '/edit/:id',
    name: 'Edit',
    component: () => import('./views/EditUserPage.vue')
  }
]
const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})
export default router