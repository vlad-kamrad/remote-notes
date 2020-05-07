import React, { useState } from 'react';
import { Button, Spinner, Form, Modal } from 'react-bootstrap';
import { notification } from 'antd';
import { MESSAGES } from '../../constants';
import { format } from 'date-fns';
import ConfirmationModal from '../ConfirmationModal/ConfirmationModal';
import './CurrentNoteModal.css';

import useDeleteNote from '../../hooks/useDeleteNote';
import useEditNote from '../../hooks/useEditNote';

const CurrentNoteModal = ({ note, show, onHide }) => {
  const { title, text, createdDate, modifiedDate } = note || '';
  const [{ titleVal, textVal }, setState] = useState({
    titleVal: title,
    textVal: text
  });

  const [editing, setEditing] = useState(false);
  const [deleting, setDeleting] = useState(false);
  const [cDeleting, setCDeleting] = useState(false);

  const showError = () => {
    notification['error']({
      message: MESSAGES.ERROR_UPDATE_NOTE,
      description: MESSAGES.ERROR_UPDATE_NOTE_TEXT
    });
  };

  const showSuccess = description => {
    notification['success']({
      message: MESSAGES.SUCCESS,
      description
    });
  };

  useDeleteNote(
    note,
    deleting,
    () => {
      setDeleting(false);
      onHide();
      showSuccess(MESSAGES.SUCCESS_DELETED_TEXT);
    },
    () => {}
  );

  useEditNote(
    {
      id: note.id,
      text: textVal,
      title: titleVal
    },
    editing,
    () => {
      setEditing(false);
      onHide();
      showSuccess(MESSAGES.SUCCESS_MODIFIED_TEXT);
    },
    () => {
      setEditing(false);
      onHide();
      showError();
    }
  );

  const onCancelEdit = () => setState({ titleVal: title, textVal: text });
  const onSaveChanges = () => setEditing(true);

  const isChangedDate = () => title !== titleVal || text !== textVal;
  const formatDate = date => format(new Date(date), 'dd/MM/yyyy HH:mm:ss');
  const isModified = date => new Date(date).getYear() > 0;

  const renderBody = () => {
    return (
      <>
        <Form.Group controlId='title'>
          <Form.Label>Title note</Form.Label>
          <Form.Control
            type='text'
            placeholder='Example title...'
            value={titleVal}
            onChange={e => {
              const value = e.target.value;
              setState(p => ({ ...p, titleVal: value }));
            }}
          />
        </Form.Group>
        <Form.Group controlId='exampleForm.ControlTextarea1'>
          <Form.Label>Text note</Form.Label>
          <Form.Control
            as='textarea'
            rows='10'
            placeholder='Example text...'
            value={textVal}
            onChange={e => {
              const value = e.target.value;
              setState(p => ({ ...p, textVal: value }));
            }}
          />
        </Form.Group>

        <Form.Label>{`Date Created: ${formatDate(createdDate)}`}</Form.Label>

        {isModified(modifiedDate) && (
          <Form.Label>{`Modified Date: ${formatDate(
            modifiedDate
          )}`}</Form.Label>
        )}
      </>
    );
  };

  const renderFooter = () => {
    const isChanged = isChangedDate();
    return (
      <div className='btn-wrapper'>
        <Button variant='light' onClick={() => setCDeleting(true)}>
          Delete Note
        </Button>
        <div>
          <Button
            variant='secondary'
            onClick={isChanged ? onCancelEdit : onHide}
          >
            {isChanged ? 'Cancel' : 'Close'}
          </Button>
          {isChanged && (
            <Button variant='primary' onClick={onSaveChanges}>
              {editing ? (
                <Spinner
                  as='span'
                  animation='border'
                  size='sm'
                  role='status'
                  aria-hidden='true'
                />
              ) : (
                'Save changes'
              )}
            </Button>
          )}
        </div>
      </div>
    );
  };

  return (
    <>
      <Modal show={show} onHide={onHide} size='lg' centered>
        <Modal.Header closeButton>
          <Modal.Title id='contained-modal-title-vcenter'>
            Note Details
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>{renderBody()}</Modal.Body>
        <Modal.Footer>{renderFooter()}</Modal.Footer>
      </Modal>
      <ConfirmationModal
        show={cDeleting}
        onHide={() => setCDeleting(false)}
        header='Are you sure you want to delete this entry?'
        ok={() => setDeleting(true)}
      />
    </>
  );
};

export default CurrentNoteModal;
