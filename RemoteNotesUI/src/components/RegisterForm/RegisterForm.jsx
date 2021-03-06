import React, { useState, useEffect } from 'react';
import { Form, Button, Spinner, Alert } from 'react-bootstrap';
import Auth from '../../utils/Auth';

import { MESSAGES } from '../../constants';
import redirectTo from '../../utils/redirectTo';
import './RegisterForm.css';

const RegisterForm = () => {
  const [{ name, surname, password, email }, setState] = useState({});
  const [pending, setPending] = useState(false);
  const [errorMessage, setErrorMessage] = useState(null);

  const onChangeHandler = e => handler => handler(e.target.value);
  const onChangeUsername = name => setState(p => ({ ...p, name }));
  const onChangeSurname = surname => setState(p => ({ ...p, surname }));
  const onChangePassword = password => setState(p => ({ ...p, password }));
  const onChangeEmail = email => setState(p => ({ ...p, email }));

  const onRegister = () => {
    if (!name || !surname || !password || !email) {
      setErrorMessage(MESSAGES.EMPTY_FIRLDS);
      return;
    }

    setPending(true);
  };

  useEffect(() => {
    if (pending) {
      (async () => {
        const body = { name, surname, password, email };

        await Auth.register(body)
          .then(() => redirectTo.index())
          .catch(status => {
            setPending(false);
            setErrorMessage(MESSAGES.REGISTER_VALID_PARAMS);
          });
      })();
    }
  }, [pending]);

  return (
    <>
      {errorMessage && (
        <Alert key='error' variant='danger'>
          {errorMessage}
        </Alert>
      )}
      <div className='container'>
        <Form.Group>
          <Form.Label>Username</Form.Label>
          <Form.Control
            type='text'
            placeholder='Enter username'
            onChange={e => onChangeHandler(e)(onChangeUsername)}
          />
        </Form.Group>

        <Form.Group>
          <Form.Label>Surname</Form.Label>
          <Form.Control
            type='text'
            placeholder='Enter surname'
            onChange={e => onChangeHandler(e)(onChangeSurname)}
          />
        </Form.Group>

        <Form.Group>
          <Form.Label>Password</Form.Label>
          <Form.Control
            type='password'
            placeholder='Password'
            onChange={e => onChangeHandler(e)(onChangePassword)}
          />
        </Form.Group>

        <Form.Group>
          <Form.Label>Email address</Form.Label>
          <Form.Control
            type='email'
            placeholder='Enter email'
            onChange={e => onChangeHandler(e)(onChangeEmail)}
          />
          <Form.Text className='text-muted'>
            We'll never share your email with anyone else.
          </Form.Text>
        </Form.Group>
        <Button variant='primary' onClick={onRegister} className='submitBtn'>
          {pending ? (
            <Spinner
              as='span'
              animation='border'
              size='sm'
              role='status'
              aria-hidden='true'
            />
          ) : (
            'Registration'
          )}
        </Button>
        <Button variant='light' onClick={redirectTo.login}>
          Login
        </Button>
      </div>
    </>
  );
};

export default RegisterForm;
