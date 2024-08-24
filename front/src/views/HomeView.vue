<script setup>
import OauthService from '../api/Oauth'
const handleRegister = async () => {
  try {
    const redirectionUrl = await OauthService.getRedirectionUrl()
    // Redirect the user to the URL
    window.location.href = redirectionUrl
  } catch (error) {
    console.error('Error during registration:', error)
  }
}
</script>

<template>
  <main>
    <button @click="handleRegister">Register</button>
  </main>
</template>
