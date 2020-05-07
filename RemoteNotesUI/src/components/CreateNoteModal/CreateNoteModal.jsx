import React, { useState, useEffect, useContext } from 'react';
import { Card, Alert, Button, Spinner, Form, Modal } from 'react-bootstrap';
import { notification } from 'antd';
import Auth from '../../utils/Auth';
import { MESSAGES } from '../../constants';
import redirectTo from '../../utils/redirectTo';
import Header from '../Header/Header';
import HttpRequest from '../../utils/HttpRequest';
import { BASE_URL, API_ENDPOINTS } from '../../constants';

import NotesContext from '../../contexts/NotesContext/NotesContext';

const createNoteUrl = BASE_URL + API_ENDPOINTS.notes;

const CreateNoteModal = ({ show, onHide }) => {
  const [{ text, title, pending }, setState] = useState({
    pending: false
  });
  const [errorMessage, setErrorMessage] = useState(null);
  const [notesState, setNotes] = useContext(NotesContext);

  const onChangeHandler = e => handler => handler(e.target.value);
  const onChangeText = text => setState(p => ({ ...p, text }));
  const onChangeTitle = title => setState(p => ({ ...p, title }));

  const showCreated = () => {
    notification['success']({
      message: MESSAGES.SUCCESS,
      description: MESSAGES.SUCCESS_CREATED_TEXT
    });
  };

  useEffect(() => {
    if (pending) {
      (async () => {
        const body = { text, title };

        await HttpRequest.post(createNoteUrl, body)
          .then(response => {
            setState(p => ({ ...p, pending: false }));
            setNotes(p => ({
              ...p,
              notes: [
                ...notesState.notes,
                { ...body, createdDate: new Date(), id: response }
              ]
            }));
            onHide();
            showCreated();
          })
          .catch(() => {
            setErrorMessage(MESSAGES.NOTE_VALID_PARAMS);
            setState(p => ({ ...p, pending: false }));
          });
      })();
    }
  }, [pending]);

  const createNote = () => {
    console.log([text, title]);
    if (!title) {
      setErrorMessage(MESSAGES.EMPTY_FIRLDS);
      return;
    }
    setState(p => ({ ...p, pending: true }));
  };

  const renderBody = () => (
    <>
      {errorMessage && (
        <Alert key='error' variant='danger'>
          {errorMessage}
        </Alert>
      )}
      <Form.Group controlId='title'>
        <Form.Label>Title note</Form.Label>
        <Form.Control
          type='text'
          placeholder='Example title...'
          onChange={e => onChangeHandler(e)(onChangeTitle)}
        />
      </Form.Group>
      <Form.Group controlId='exampleForm.ControlTextarea1'>
        <Form.Label>Text note</Form.Label>
        <Form.Control
          as='textarea'
          rows='10'
          placeholder='Example text...'
          onChange={e => onChangeHandler(e)(onChangeText)}
        />
      </Form.Group>
    </>
  );

  return (
    <Modal show={show} onHide={onHide} size='lg' centered>
      <Modal.Header closeButton>
        <Modal.Title id='contained-modal-title-vcenter'>
          Create Note
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>{renderBody()}</Modal.Body>
      <Modal.Footer>
        <Button variant='secondary' onClick={onHide}>
          Close
        </Button>
        <Button variant='primary' onClick={createNote}>
          Save changes
        </Button>
      </Modal.Footer>
    </Modal>
  );
};

export default CreateNoteModal;
