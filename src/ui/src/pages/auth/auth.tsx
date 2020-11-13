import React from 'react';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import axios from 'axios';
import { Form, Input, Button } from 'antd';

import { login } from '../../utils/auth/auth-provider';

import styles from './auth.module.scss';
import { useHistory } from 'react-router-dom';

const validationSchema = Yup.object({
    nickname: Yup.string().min(3, 'Minimum name length 3 characters.').max(20, 'Maximum name length 20 characters.').required('This field is required.'),
    password: Yup.string().required('This field is required.')
})

const Auth = () => {
    return (
        <div className={styles['auth-page']}>

            <div className={styles['auth-page__application-name']}>
                Chatter
                </div>
            <div className={styles['auth-page__form']}>
                <LoginForm />
            </div>
        </div>
    )
}

const LoginForm: React.FC = () => {
    const history = useHistory()

    const formik = useFormik({
        initialValues: {
            nickname: '',
            password: ''
        },
        onSubmit: async (values, formikHelpers) => {
            const result = await axios.post('http://localhost:5000/api/authentication/authenticate', values)
            login(result.data)
            formikHelpers.setSubmitting(false);
        },
        validationSchema
    });

    const onRegister = () => {
        history.push('/register')
    }

    return (
        <form onSubmit={formik.handleSubmit}>
            <div className="form-control">
                <div className={styles['form-control__label']}>Nickname</div>
                <Input
                    type='text'
                    id='nickname'
                    name='nickname'
                    onChange={formik.handleChange}
                    value={formik.values.nickname}
                    onBlur={formik.handleBlur}
                />
                {
                    formik.errors.nickname && formik.touched.nickname ? <div className="form-error">{formik.errors.nickname}</div> : null
                }
            </div>

            <div className="form-control">
                <div className={styles['form-control__label']}>Password</div>
                <Input.Password
                    type='password'
                    id='password'
                    name='password'
                    onChange={formik.handleChange}
                    value={formik.values.password}
                    onBlur={formik.handleBlur
                    } />
                {
                    formik.errors.password && formik.touched.password ? <div className="form-error">{formik.errors.password}</div> : null
                }
            </div>

            <div className={styles['buttons']}>
                <Button disabled={formik.isSubmitting} onClick={onRegister} type="primary" shape="round" size={'middle'}>
                    Register
                </Button>
                <Button disabled={formik.isSubmitting} htmlType='submit' type="primary" shape="round" size={'middle'}>
                    Login
                </Button>
            </div>
        </form>
    )
}

export default Auth;