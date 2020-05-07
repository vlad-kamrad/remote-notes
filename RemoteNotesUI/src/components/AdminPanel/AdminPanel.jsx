import React, { useState, useEffect, useContext } from 'react';
import { Card, ListGroup, Button, Spinner, Alert } from 'react-bootstrap';
import { List, IconText } from 'antd';
import Auth from '../../utils/Auth';
import { MESSAGES } from '../../constants';
import redirectTo from '../../utils/redirectTo';
import Header from '../Header/Header';
import { BASE_URL, API_ENDPOINTS } from '../../constants';
import HttpRequest from '../../utils/HttpRequest';
import NotesContext from '../../contexts/NotesContext/NotesContext';

import CreateNoteModal from '../CreateNoteModal/CreateNoteModal';
import CurrentNoteModal from '../CurrentNoteModal/CurrentNoteModal';

const getNotesUrl = BASE_URL + API_ENDPOINTS.notes;

const AdminPanel = () => {
  const [isShowCreatedModal, setShowCreateModal] = useState(false);
  const [loading, setLoading] = useState(false);
  const [notesState, setNotes] = useContext(NotesContext);

  useEffect(() => {
    !Auth.isSignedIn() && redirectTo.login();
  }, []);

  const renderItem = item => (
    <List.Item
      key={item.title}
      onClick={() => {}}
    >
      <List.Item.Meta title={item.title} />
      {item.text}
    </List.Item>
  );

  const renderBody = () => {
    return (
      <div className='body'>
        <Button variant='primary' onClick={() => setShowCreateModal(true)}>
          Create note
        </Button>
        <div className='notes'>
          <List
            loading={loading}
            itemLayout='vertical'
            pagination={{
              onChange: page => console.log(page),
              pageSize: 5
            }}
            dataSource={notesState.notes.sort((a, b) => b.id - a.id)}
            renderItem={renderItem}
          />
        </div>
      </div>
    );
  };

  return (
    <>
      <Header />
      {renderBody()}
    </>
  );
};

export default AdminPanel;
