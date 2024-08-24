import axiosInstance from './main'; // Importing the axios instance

const CalendarService = {
  /**
   * Get all calendar events
   * @returns {Promise} Axios response promise with the list of events
   */
  getEvents() {
    return axiosInstance.get('/calendar')
      .then(response => response.data)
      .catch(error => {
        console.error('There was an error fetching events:', error);
        throw error;
      });
  },

  /**
   * Add a new calendar event
   * @param {Object} eventData The calendar event data to be added
   * @returns {Promise} Axios response promise with the created event
   */
  addEvent(eventData) {
    return axiosInstance.post('/calendar', eventData)
      .then(response => response.data)
      .catch(error => {
        console.error('There was an error adding the event:', error);
        throw error;
      });
  },

  /**
   * Update an existing calendar event
   * @param {string} id The ID of the calendar event to update
   * @param {Object} eventData The updated event data
   * @returns {Promise} Axios response promise
   */
  updateEvent(id, eventData) {
    return axiosInstance.put(`/calendar/${id}`, eventData)
      .then(response => response.data)
      .catch(error => {
        console.error('There was an error updating the event:', error);
        throw error;
      });
  },

  /**
   * Delete a calendar event
   * @param {string} id The ID of the calendar event to delete
   * @returns {Promise} Axios response promise
   */
  deleteEvent(id) {
    return axiosInstance.delete(`/calendar/${id}`)
      .then(response => response.data)
      .catch(error => {
        console.error('There was an error deleting the event:', error);
        throw error;
      });
  }
};

export default CalendarService;
