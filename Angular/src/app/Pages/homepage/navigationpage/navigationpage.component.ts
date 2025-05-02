import { Component } from '@angular/core';

@Component({
  selector: 'app-navigationpage',
  standalone: true,
  imports: [],
  templateUrl: './navigationpage.component.html',
  styleUrl: './navigationpage.component.css'
})
export class NavigationpageComponent {

  courses = [
    { name: 'C', image: 'download.jpg', description: 'Learn the fundamentals of C programming for systems and embedded development.' },
    { name: 'C++', image: 'download.jpg', description: 'Master OOP and STL with C++ for high-performance applications.' },
    { name: 'Python', image: 'download.jpg', description: 'Get started with Python for data science, automation, and web apps.' },
    { name: 'Dot Net', image: 'download.jpg', description: 'Build robust enterprise apps using .NET and C#.' },
    { name: 'Java', image: 'download.jpg', description: 'Write secure and portable applications using Java and Spring.' },
    { name: 'Java Script', image: 'download.jpg', description: 'Make interactive web pages with modern JavaScript.' },
    { name: 'HTML', image: 'download.jpg', description: 'Structure websites using HTML5 best practices.' },
    { name: 'CSS', image: 'download.jpg', description: 'Design stunning webpages with modern CSS3 techniques.' },
    { name: 'DBMS', image: 'download.jpg', description: 'Understand relational databases and SQL queries.' },
    { name: 'DSA', image: 'download.jpg', description: 'Crack coding interviews by mastering algorithms and data structures.' },
    { name: 'Frontend', image: 'download.jpg', description: 'Build sleek UIs with Angular, React, and more.' },
    { name: 'Backend', image: 'download.jpg', description: 'Power your apps with secure and scalable backend systems.' },
    { name: 'Full-Stack', image: 'download.jpg', description: 'Be a complete developer with full-stack capabilities.' },
    { name: 'Machine Learning', image: 'download.jpg', description: 'Create intelligent systems using supervised and unsupervised learning.' },
    { name: 'Deep Learning', image: 'download.jpg', description: 'Build neural networks with TensorFlow and PyTorch.' },
    { name: 'Data Scientist', image: 'download.jpg', description: 'Uncover insights and drive business decisions with data.' },
    { name: 'Artificial Intelligence', image: 'download.jpg', description: 'Explore the world of smart agents and decision systems.' },
    { name: 'Data Engineer', image: 'download.jpg', description: 'Design and maintain data pipelines and storage systems.' },
    { name: 'Software Engineer', image: 'download.jpg', description: 'Master the software lifecycle and agile methodologies.' },
  ];
}
