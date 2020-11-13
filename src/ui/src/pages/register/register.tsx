import React from 'react';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import axios from 'axios';
import { useHistory } from 'react-router-dom';
import { Button, Input } from 'antd';

import styles from './register.module.scss';

const validationSchema = Yup.object({
    nickname: Yup.string().min(3, 'Minimum name length 3 characters.').max(20, 'Maximum name length 20 characters.').required('This field is required.'),
    password: Yup.string().required('This field is required.'),
    confirmPassword: Yup.string().oneOf([Yup.ref('password')], 'Passwords must be equal').required('This field is required.')
})

const Register: React.FC = () => {
    return (
        <div className={styles['register-page']}>

            <div className={styles['register-page__application-name']}>
                Chatter
                </div>
            <div className={styles['register-page__form']}>
                <RegisterForm />
            </div>
        </div>
    )
}

const RegisterForm: React.FC = () => {
    const history = useHistory()

    const formik = useFormik({
        initialValues: {
            nickname: '',
            password: '',
            confirmPassword: ''
        },

        onSubmit: async (values, formikHelpers) => {
            await axios.post('http://localhost:5000/api/account', {
                nickname: values.nickname,
                password: values.password
            })

            history.push('/login');
        },
        validationSchema
    });

    const onLogin = () => {
        history.push('/login')
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

            <div className="form-control">
                <div className={styles['form-control__label']}>Confirm password</div>
                <Input.Password
                    type='password'
                    id='confirmPassword'
                    name='confirmPassword'
                    onChange={formik.handleChange}
                    value={formik.values.confirmPassword}
                    onBlur={formik.handleBlur
                    } />
                {
                    formik.errors.confirmPassword && formik.touched.confirmPassword ? <div className="form-error">{formik.errors.confirmPassword}</div> : null
                }
            </div>

            <div className={styles['buttons']}>
                <Button disabled={formik.isSubmitting} onClick={onLogin} type="primary" shape="round" size={'middle'}>
                    Login
                </Button>
                <Button htmlType='submit' disabled={formik.isSubmitting} type="primary" shape="round" size={'middle'}>
                    Register
                </Button>
            </div>
        </form>
    )
}

export default Register;