import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import axios from 'axios'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('../views/AboutView.vue')
    },
    {
      path: '/redirect',
      name: 'redirect',
      component: {
        template: '<div>Redirecting...</div>',
        created() {
          // Make a request to your API to get the redirect URL
          axios
            .get('/your-api/authorize')
            .then((response) => {
              if (response.data.redirectUrl) {
                // Open the redirect URL in a new tab
                window.open(response.data.redirectUrl, '_blank')
                // Optionally, navigate back to the home page or another page
                this.$router.push({ name: 'home' })
              }
            })
            .catch((error) => {
              console.error('Error during redirection:', error)
              // Handle the error (e.g., display a message, redirect elsewhere)
            })
        }
      }
    }
  ]
})
export default router
