import React, { useState } from 'react';
import PropTypes from 'prop-types';

import NotesContext from './NotesContext';

const NotesProvider = ({ initialState, children }) => {
  const [note, setNote] = useState(initialState);
  return (
    <NotesContext.Provider value={[note, setNote]}>
      {children}
    </NotesContext.Provider>
  );
};

/* NotesProvider.propTypes = {
  initialState: PropTypes.object,
  children: PropTypes.oneOfType([
    PropTypes.arrayOf(PropTypes.node),
    PropTypes.node
  ])
}; */

export default NotesProvider;
