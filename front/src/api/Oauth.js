import axiosInstance from './main' // Assuming axiosInstance is exported from client.js

const OauthService = {
  /**
   * Get the OAuth redirection URL from the backend.
   * @returns {Promise} A promise that resolves with the redirection URL.
   */
  getRedirectionUrl() {
    return axiosInstance
      .get('/oauth/getRedirectionUrl')
      .then((response) => response.data)
      .catch((error) => {
        console.error('Failed to get the OAuth redirection URL:', error)
        throw error
      })
  }
}

export default OauthService
