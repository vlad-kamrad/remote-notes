export { default as API_ENDPOINTS } from './apiEndpoints';
export { default as MESSAGES } from './messages';

export const BASE_URL = 'https://localhost:44318/api';

export const initialNotesState = {
  notes: [],
  notesLoading: false,
  currentUser: {
    username: null,
    userRoles: [],
    isAdmin: false
  }
};
