import React, { useState } from 'react';
import axios from 'axios';

const App: React.FC = () => {
  const [email, setEmail] = useState<string>('');
  const [file, setFile] = useState<File | null>(null);
  const [errorMessage, setErrorMessage] = useState<string>('');

  const handleEmailChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setEmail(e.target.value);
  };

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const selectedFile = e.target.files && e.target.files[0];
    if (selectedFile && selectedFile.name.endsWith('.docx')) {
      setFile(selectedFile);
      setErrorMessage('');
    } else {
      setFile(null);
      setErrorMessage('Please select a .docx file.');
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!email.trim()) {
      setErrorMessage('Please enter your email.');
      return;
    }
    if (!file) {
      setErrorMessage('Please select a .docx file.');
      return;
    }
    try {
      const formData = new FormData();
      formData.append('email', email);
      formData.append('file', file);
      // Замініть URL на URL вашого REST API контролера
      const response = await axios.post('https://notifywebapplication.azurewebsites.net/api/Notify', formData, {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      });
      console.log('Response:', response.data);
      // Очистка форми після успішного відправлення
      setEmail('');
      setFile(null);
      setErrorMessage('');
    } catch (error) {
      console.error('Error:', error);
      // Обробка помилок
    }
  };

  return (
    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Email:</label>
          <input type="email" value={email} onChange={handleEmailChange} />
        </div>
        <div>
          <label>Upload .docx file:</label>
          <input type="file" accept=".docx" onChange={handleFileChange} />
        </div>
        {errorMessage && <div style={{ color: 'red' }}>{errorMessage}</div>}
        <button type="submit">Submit</button>
      </form>
    </div>
  );
};

export default App;

