import React, { useState, useEffect } from 'react';
import { Form, Button } from 'react-bootstrap';
import { notification } from 'antd';
import HttpRequest from '../../utils/HttpRequest';
import { MESSAGES } from '../../constants';
import redirectTo from '../../utils/redirectTo';
import { BASE_URL, API_ENDPOINTS } from '../../constants';
import Header from '../Header/Header';
//import './RegisterForm.css';

const getUserInfoUrl = BASE_URL + API_ENDPOINTS.getUserInfo;
const changeUserInfoUrl = BASE_URL + API_ENDPOINTS.users;

const Settings = () => {
  const [
    { id, name, surname, email, modified, created },
    setOldSettings
  ] = useState({});
  const [{ nName, nSurname, nEmail, nPass }, setNewSettings] = useState({
    nPass: null
  });

  const [isReady, setReadyForEdit] = useState(false);
  const [changePassword, setChangePassword] = useState(false);

  useEffect(() => {
    (async () => {
      await HttpRequest.get(getUserInfoUrl).then(
        ({ id, name, surname, email, modified, created }) => {
          setOldSettings({
            id,
            name,
            surname,
            email,
            modified,
            created
          });
          setNewSettings(p => ({
            ...p,
            nName: name,
            nSurname: surname,
            nEmail: email
          }));
        }
      );
    })();
  }, []);

  useEffect(() => {
    if (
      nName !== name ||
      nSurname !== surname ||
      nEmail !== email ||
      (changePassword && nPass)
    ) {
      setReadyForEdit(true);
    } else {
      setReadyForEdit(false);
    }
  }, [nName, nSurname, nEmail, nPass]);

  const handleInputChange = event => {
    const { target } = event;
    const { name, value } = target;
    setNewSettings(p => ({
      ...p,
      [name]: value
    }));
  };

  const showNotification = success => {
    notification[success ? 'success' : 'error']({
      message: success ? MESSAGES.SUCCESS : MESSAGES.FAILED,
      placement: 'bottomLeft'
    });
  };

  return (
    <>
      <Header />
      <div className='container'>
        <Form.Group>
          <Form.Label>{`Creation Date: ${created}`}</Form.Label>
          {modified && <Form.Label>{`Modified Date: ${modified}`}</Form.Label>}
        </Form.Group>
        <Form.Group>
          <Form.Label>Your Username</Form.Label>
          <Form.Control type='text' placeholder={id} disabled={true} />
        </Form.Group>

        <Form.Group>
          <Form.Label>Your Username</Form.Label>
          <Form.Control
            name='nName'
            type='text'
            placeholder='Enter username'
            onChange={handleInputChange}
            defaultValue={name}
          />
        </Form.Group>

        <Form.Group>
          <Form.Label>Your Surname</Form.Label>
          <Form.Control
            name='nSurname'
            type='text'
            placeholder='Enter surname'
            onChange={handleInputChange}
            defaultValue={surname}
          />
        </Form.Group>
        <Form.Group>
          <Form.Label>Your Email Address</Form.Label>
          <Form.Control
            name='nEmail'
            type='email'
            placeholder='Enter email'
            onChange={handleInputChange}
            defaultValue={email}
          />
        </Form.Group>

        <Form.Group>
          <Form.Check
            type='checkbox'
            label='Change password'
            defaultValue={changePassword}
            onChange={() => setChangePassword(!changePassword)}
          />
        </Form.Group>

        {changePassword && (
          <Form.Group>
            <Form.Label>New Password</Form.Label>
            <Form.Control
              name='nPass'
              type='password'
              placeholder='Password'
              onChange={handleInputChange}
            />
          </Form.Group>
        )}

        {isReady && (
          <>
            <Button
              variant='primary'
              className='submitBtn'
              style={{ width: '130px' }}
              onClick={async () => {
                await HttpRequest.put(changeUserInfoUrl, {
                  name: nName !== name ? nName : null,
                  surname: nSurname,
                  email: nEmail,
                  password: nPass
                })
                  .then(res => {
                    if (res) {
                      showNotification(true);
                      window.location.reload();
                    } else {
                      showNotification(false);
                    }
                  })
                  .catch(() => {
                    showNotification(false);
                  });
              }}
            >
              Save Changes
            </Button>
            <Button variant='light' onClick={redirectTo.index}>
              Cancel
            </Button>
          </>
        )}
      </div>
    </>
  );
};

export default Settings;
