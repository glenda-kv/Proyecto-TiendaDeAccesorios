import type { ReactNode } from "react";
import { Header } from "./Header";

interface LayoutProps {
  children: ReactNode;
}

export function Layout({ children }: LayoutProps) {
  return (
    <>
      <Header />

      <main className="container py-5">
        <section className="bg-white rounded-4 shadow p-4">
          {children}
        </section>
      </main>
    </>
  );
}