import { useEffect, useContext } from 'react';
import HttpRequest from '../utils/HttpRequest';
import { BASE_URL, API_ENDPOINTS } from '../constants/index';
import NotesContext from '../contexts/NotesContext/NotesContext';

const updateNoteUrl = BASE_URL + API_ENDPOINTS.notes;

const useEditNote = (mNote, editing, resolve, reject) => {
  const [notesState, setNotes] = useContext(NotesContext);

  useEffect(() => {
    if (editing) {
      (async () => {
       /*  const body = {
          id: note.id,
          text: mNote.text,
          title: mNote.title
        }; */

        await HttpRequest.put(updateNoteUrl, mNote)
          .then(() => {
            // Use Optimistic update
            const notes = notesState.notes.map(x =>
              x.id != mNote.id
                ? x
                : {
                    ...x,
                    text: mNote.text,
                    title: mNote.title,
                    modifiedDate: new Date()
                  }
            );

            setNotes(p => ({ ...p, notes }));
            resolve();
            /* 
            setEditing(false);
            onHide();
            showSuccess(MESSAGES.SUCCESS_MODIFIED_TEXT); */
          })
          .catch(
            reject /* () => {
            // Rollback
            setEditing(false);
            onHide();
            showError();
          } */
          );
      })();
    }
  }, [editing]);

  return null;
};

export default useEditNote;
