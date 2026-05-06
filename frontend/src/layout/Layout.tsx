import type { ReactNode } from 'react';
import { Header } from './Header';

interface LayoutProps {
  children: ReactNode;
}

export function Layout({ children }: LayoutProps) {
  return (
    <>
      <Header />
      <main className="container py-5 app-shell">
        <section className="bg-white rounded-4 shadow-sm p-4">
          {children}
        </section>
      </main>
    </>
  );
}