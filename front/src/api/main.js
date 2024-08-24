import axios from 'axios';
import router from '../router/index'; // Assuming you have Vue Router set up

// Create an Axios instance
const axiosInstance = axios.create({
  baseURL: 'https://localhost:7180/api', // Replace with your backend URL
  timeout: 5000, // You can set a timeout for requests
});
axiosInstance.interceptors.request.use(
    (config) => {
      // Add headers or authentication tokens if necessary
      return config;
    },
    (error) => {
      return Promise.reject(error);
    }
  );
  
  // Intercept responses
  axiosInstance.interceptors.response.use(
    (response) => {
        console.log('response', response);
      // If a 302 redirect is received, you can handle it here
      if (response.status === 302) {
        const redirectUrl = response.headers.location;
        
        // If a redirect URL is provided in the response header, navigate to it
        if (redirectUrl) {
          window.location.href = redirectUrl;
        } else {
          // Optionally handle the situation where the redirect URL is missing
          console.warn('302 received without a redirect URL.');
        }
      }
  
      return response;
    },
    (error) => {
        console.log('error', error);

      // Manage error responses
      if (error.response) {
        const status = error.response.status;
  
        // Status code management
        if (status === 401) {
          // Unauthorized, redirect to login page
          router.push('/login');
        } else if (status === 403) {
          // Forbidden, handle accordingly
          alert('You do not have the necessary permissions.');
        } else if (status === 404) {
          // Not found, redirect to a 404 page
          router.push('/404');
        } else if (status === 302) {
          // Handle redirect manually in case of 302 error
          const redirectUrl = error.response.headers.location;
          if (redirectUrl) {
            window.location.href = redirectUrl;
          }
        } else if (status >= 500) {
          // Server error, handle accordingly
          alert('A server error occurred. Please try again later.');
        }
      }
  
      return Promise.reject(error);
    }
  );
  
  export default axiosInstance;