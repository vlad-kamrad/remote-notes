import { useEffect, useContext } from 'react';
import HttpRequest from '../utils/HttpRequest';
import { BASE_URL, API_ENDPOINTS } from '../constants/index';
import NotesContext from '../contexts/NotesContext/NotesContext';

const deleteNoteUrl = BASE_URL + API_ENDPOINTS.notes;

const useDeleteNote = (note, deleting, resolve, reject) => {
  const [notesState, setNotes] = useContext(NotesContext);
  useEffect(() => {
    if (deleting) {
      (async () => {
        await HttpRequest.delete(deleteNoteUrl, { id: note.id })
          .then(res => {
            console.log(res);

            const notes = [];
            notesState.notes.forEach(x => {
              if (x.id != note.id) notes.push(x);
            });

            setNotes(p => ({ ...p, notes }));
            resolve();
          })
          .catch(reject);
      })();
    }
  }, [deleting]);

  return null;
};

export default useDeleteNote;
