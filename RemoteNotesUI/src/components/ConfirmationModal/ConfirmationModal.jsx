import React from 'react';
import { Button, Modal } from 'react-bootstrap';

import './ConfirmationModal.css';

const ConfirmationModal = ({ show, onHide, header, ok }) => {
  return (
    <>
      <Modal
        show={show}
        onHide={onHide}
        centered
        backdrop={true}
        className='confirm'
      >
        <Modal.Header closeButton>
          <Modal.Title>{header}</Modal.Title>
        </Modal.Header>
        <Modal.Footer>
          <Button
            variant='light'
            onClick={onHide}
          >
            Cancel
          </Button>
          <Button
            variant='primary'
            onClick={() => {
              onHide();
              ok();
            }}
          >
            Ok
          </Button>
        </Modal.Footer>
      </Modal>
      {show && <div className='shadow' />}
    </>
  );
};

export default ConfirmationModal;
